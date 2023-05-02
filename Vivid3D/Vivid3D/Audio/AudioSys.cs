using OpenTK.Audio.OpenAL;
using OpenTK.Audio.OpenAL.Extensions;
using OpenTK.Audio.OpenAL.Extensions.Creative;
using OpenTK.Audio.OpenAL.Extensions.EXT;
using OpenTK.Audio.OpenAL.Extensions.SOFT;
using System.Collections;

namespace Vivid.Audio
{
    public static class AudioSys
    {
        public static ALDevice Device;
        public static ALContext Context;
        public static void Init()
        {
            Console.WriteLine("Hello!");
            var devices = ALC.GetStringList(GetEnumerationStringList.DeviceSpecifier);
            Console.WriteLine($"Devices: {string.Join(", ", devices)}");

            // Get the default device, then go though all devices and select the AL soft device if it exists.
            string deviceName = ALC.GetString(ALDevice.Null, AlcGetString.DefaultDeviceSpecifier);
            foreach (var d in devices)
            {
                if (d.Contains("OpenAL Soft"))
                {
                    deviceName = d;
                }
            }

            var allDevices = OpenTK.Audio.OpenAL.Extensions.Creative.EnumerateAll.EnumerateAll.GetStringList(OpenTK.Audio.OpenAL.Extensions.Creative.EnumerateAll.GetEnumerateAllContextStringList.AllDevicesSpecifier);
            Console.WriteLine($"All Devices: {string.Join(", ", allDevices)}");

            var device = ALC.OpenDevice(deviceName);
            var context = ALC.CreateContext(device, (int[])null);
            ALC.MakeContextCurrent(context);

            CheckALError("Start");

            ALC.GetInteger(device, AlcGetInteger.MajorVersion, 1, out int alcMajorVersion);
            ALC.GetInteger(device, AlcGetInteger.MinorVersion, 1, out int alcMinorVersion);
            string alcExts = ALC.GetString(device, AlcGetString.Extensions);

            var attrs = ALC.GetContextAttributes(device);
            Console.WriteLine($"Attributes: {attrs}");

            string exts = AL.Get(ALGetString.Extensions);
            string rend = AL.Get(ALGetString.Renderer);
            string vend = AL.Get(ALGetString.Vendor);
            string vers = AL.Get(ALGetString.Version);
            Device = device;
            Context = context;
        }

        public static void CheckALError(string str)
        {
            ALError error = AL.GetError();
            if (error != ALError.NoError)
            {
                Console.WriteLine($"ALError at '{str}': {AL.GetErrorString(error)}");
            }
        }

        public static void FillSine(short[] buffer, float frequency, float sampleRate)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (short)(MathF.Sin((i * frequency * MathF.PI * 2) / sampleRate) * short.MaxValue);
            }
        }

    }

    public class Channel
    {
        
        public Sound SoundOwner
        {
            get;
            set;
        }

        public int Source
        {
            get;
            set;
        }

        public int Buffer
        {
            get;
            set;
        }

        public Channel(Sound sound)
        {
            SoundOwner = sound;
            int buffer = AL.GenBuffer();
            int source = AL.GenSource();
            int state;
            Buffer = buffer;
            Source = source;

            int channels, bits_per_sample, sample_rate;
            byte[] sound_data = sound.SoundData;
            unsafe
            {
                fixed (byte* ptr = sound_data)
                {
                    // Use the ptr variable here

                    AL.BufferData(buffer, Sound.GetSoundFormat(SoundOwner.Channels, SoundOwner.Bits), ptr, sound_data.Length, SoundOwner.Rate);
                }
            }
            AL.Source(source, ALSourcei.Buffer, buffer);
            AL.SourcePlay(source);
        }

        public void Stop()
        {
            // GemBridge.gem_StopSound(src);
        }

        public void SetVolume(float volume)
        {
            // GemBridge.gem_SetSoundVolume(src, volume);
        }

        public void SetPitch(float pitch)
        {
            // GemBridge.gem_SetSoundPitch(src, pitch);
        }
    }

    public class Sound
    {
        
        public byte[] SoundData
        {
            get;
            set;
        }

        public int Channels
        {
            get;
            set;
        }

        public int Bits
        {
            get;
            set;
        }

        public int Rate
        {
            get;
            set;
        }

        public Sound(string path)
        {

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            switch(Path.GetExtension(path))
            {
                case ".wav":
                 
                    int channels = 0;
                    int bits = 0;
                    int rate = 0;
                    SoundData = LoadWave(fs, out channels, out bits, out rate);
                    Channels = channels;
                    Bits = bits;
                    Rate = rate;
                    int aa = 5;
                    //LoadWavData(path);
                    break;
            }

        }
        public static byte[] LoadWave(Stream stream, out int channels, out int bits, out int rate)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            using (BinaryReader reader = new BinaryReader(stream))
            {
                // RIFF header
                string signature = new string(reader.ReadChars(4));
                if (signature != "RIFF")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                int riff_chunck_size = reader.ReadInt32();

                string format = new string(reader.ReadChars(4));
                if (format != "WAVE")
                    throw new NotSupportedException("Specified stream is not a wave file.");

                // WAVE header
                string format_signature = new string(reader.ReadChars(4));
                if (format_signature != "fmt ")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int format_chunk_size = reader.ReadInt32();
                int audio_format = reader.ReadInt16();
                int num_channels = reader.ReadInt16();
                int sample_rate = reader.ReadInt32();
                int byte_rate = reader.ReadInt32();
                int block_align = reader.ReadInt16();
                int bits_per_sample = reader.ReadInt16();

                string data_signature = new string(reader.ReadChars(4));
                if (data_signature != "data")
                    throw new NotSupportedException("Specified wave file is not supported.");

                int data_chunk_size = reader.ReadInt32();

                channels = num_channels;
                bits = bits_per_sample;
                rate = sample_rate;

                return reader.ReadBytes((int)reader.BaseStream.Length);
            }
        }

        public void LoadWavData(string path)
        {



        }

        public Sound(MemoryStream stream, string name)
        {
            byte[] data = new byte[stream.Length];

            stream.Position = 0;
            stream.Read(data, 0, (int)stream.Length);

            //  src = GemBridge.gem_LoadSoundMemory(data,(int)stream.Length,name);
        }

        public static ALFormat GetSoundFormat(int channels, int bits)
        {
            switch (channels)
            {
                case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
                case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
                default: throw new NotSupportedException("The specified sound format is not supported.");
            }
        }

        public Channel Play2D()
        {
            Channel channel = new Channel(this);
            //channel.Play();



            return channel;
        }
    }
}