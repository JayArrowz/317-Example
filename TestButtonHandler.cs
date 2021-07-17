using NetScape.Abstractions.Model.Messages;
using NetScape.Modules.Messages;
using NetScape.Modules.Messages.Models;
using System;

namespace ANewServer
{
    [MessageHandler]
    public class TestButtonHandler
    {
        [Message(typeof(ThreeOneSevenDecoderMessages.Types.ButtonMessage))]
        public void DoSomeShit(DecoderMessage<ThreeOneSevenDecoderMessages.Types.ButtonMessage> message)
        {
            Console.WriteLine("Button clicked: " + message.Message.InterfaceId);
        }
    }
}
