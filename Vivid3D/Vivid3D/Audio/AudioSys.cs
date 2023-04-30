namespace Vivid.Audio
{
    public static class AudioSys
    {
        public static void Init()
        {
            // GemBridge.gem_InitAudio();
        }
    }

    public class Channel
    {
        private IntPtr src;

        public Channel(IntPtr ptr)
        {
            src = ptr;
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
        private IntPtr src;

        public Sound(string path)
        {
            //     src = GemBridge.gem_LoadSound(path);
        }

        public Sound(MemoryStream stream, string name)
        {
            byte[] data = new byte[stream.Length];

            stream.Position = 0;
            stream.Read(data, 0, (int)stream.Length);

            //  src = GemBridge.gem_LoadSoundMemory(data,(int)stream.Length,name);
        }

        public Channel Play2D()
        {
            //   return new Channel(GemBridge.gem_PlaySound2D(src));
            return null;
        }
    }
}