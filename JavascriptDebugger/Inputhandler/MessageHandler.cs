using JavascriptDebugger.Inputhandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace JavascriptDebugger.InputHandler
{
    public class MessageHandler
    {

        public InputType[] allInputs = new InputType[5];
        
        public MessageHandler()
        {
            allInputs[0] = new GlobalUpdate();
        }

        public void HandleMessage(String json)
        {
            BasicRequest br = new BasicRequest(json);
            allInputs[br.id].Execute(br.data,br.time);
        }
    }
}
