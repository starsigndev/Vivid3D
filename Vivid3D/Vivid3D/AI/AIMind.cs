using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LLama.Common;
using LLama;
using static System.Collections.Specialized.BitVector32;

namespace Vivid.AI
{
    public class AIMind
    {
        //LLamaModel model;
        //LLama.OldVersion.ChatSession<LLama.OldVersion.LLamaModel> _session;
        InteractiveExecutor ex;
        ChatSession _session;
        public AIMind(string info,params string[] no)
        {

             ex = new InteractiveExecutor(new LLamaModel(new ModelParams("ai/models/wizardLM-7B.ggmlv3.q4_1.bin", contextSize: 800, seed: 1447, gpuLayerCount: 2000)));
            _session = new ChatSession(ex);

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

        public string AskQuestion(string text)
        {

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
            string answer = "";
            foreach (var res in _session.Chat(text, new InferenceParams() { Temperature = 0.6f, AntiPrompts = new List<string> { "User:" } }))
            {
                answer = answer + res;
            }

            return answer;
        }
    }
}
