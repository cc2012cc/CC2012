using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lib
{
    public class swfextract
    {
        protected lib.Config configManager;

        public swfextract(String mapPath)
        {
            configManager = new Config(mapPath);
        }

        public String extractText(String doc, String page)
        {
            try
            {
                String output = "";
                String swfFilePath = configManager.getConfig("path.swf") + doc + page + ".swf";
                String command = configManager.getConfig("cmd.searching.extracttext"); ;
            
                if(!Common.validSwfParams(swfFilePath,doc,page))
                    return "[Invalid Parameters]";

                command = command.Replace("{path.swf}",configManager.getConfig("path.swf"));
                command = command.Replace("{swffile}", doc + page + ".swf");

                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = command.Substring(0, command.IndexOf(".exe") + 5);
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.Arguments = command.Substring(command.IndexOf(".exe") + 5);

                if (proc.Start())
                {
                    output = proc.StandardOutput.ReadToEnd();
                    return output;
                }
                else
                    return "[Error Extracting]";
            }
            catch (Exception ex)
            {
                return "[Error Extracting]";
            }
        }
    }
}