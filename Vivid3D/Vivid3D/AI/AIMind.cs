using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLama.Common;
using LLama;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.ExceptionServices;

namespace Vivid.AI
{
    public class AIMind
    {
        public bool Answered
        {
            get;
            set;
        }
        public bool Answering = false;
        //LLamaModel model;
        //LLama.OldVersion.ChatSession<LLama.OldVersion.LLamaModel> _session;
        InteractiveExecutor ex;
        ChatSession _session;
        public Thread ThinkThread;
        public AIMind(string info,params string[] no)
        {

             ex = new InteractiveExecutor(new LLamaModel(new ModelParams("ai/models/wizardLM-7B.ggmlv3.q4_1.bin", contextSize: 8000, seed: 1447, gpuLayerCount: 8000)));
            _session = new ChatSession(ex);
            Answered = false;

            /*
             model = new LLama.OldVersion.LLamaModel(new LLamaParams(
                model: Path.Combine("AI","Models", "wizardLM-7B.ggmlv3.q4_1.bin"),
                n_ctx: 5120,
                interactive: true,
                repeat_penalty: 1.0f,
                verbose_prompt: false));

            _session = new ChatSession<LLama.OldVersion.LLamaModel>(model)
              .WithPromptFile("ai/"+info+".txt")
              .WithAntiprompt(new string[] { "!Return!"});
            */
        }
        bool first = true;
        
         void _ThinkThread(object text)
        {
            if (first)
            {
               foreach(var res in _session.Chat(intro, new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "Do not speculate" } }))
                {

                }
                
                first = false;

            }
            response.Clear();
            string tx = (string)text;
            foreach (var res in _session.Chat(tx, new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "Do not speculate" } }))
            {

                lock (ll)
                {
                    response.Add(res);
                }

                //answer = answer + res;
                //Console.Write(res);

            }
            Answered = true;
            Answering = false;
        }
        object ll = new object();
        public List<string> response = new List<string>();
        string intro;

        public List<string> GetResponses()
        {
            List<string> r = new List<string>();
            lock (ll)
            {
                foreach(var r2 in response)
                {
                    r.Add(r2);
                }
                response.Clear();
            }
            
            return r;
        }
        public string AskQuestion(string text)
        {


            intro = File.ReadAllText("ai/general.txt");
            Answered = false;
            int bb = 5;
            /*
            text += "\n";
            var outputs = _session.Chat(text, encoding: "UTF-8");
            string answer = "";
            foreach (var output in outputs)
            {
                //Console.Write(output);
                //answer.Add(output);
                answer = answer + output;
            }
            Console.WriteLine("");
            */

            Answering = true;

            ThinkThread = new Thread(new ParameterizedThreadStart(_ThinkThread));

            ThinkThread.Start((object)text);

            //ThinkThread.


            string answer = "";
          

            return answer;
        }
    }
}
