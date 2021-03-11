using System;
using System.Collections.Generic;

namespace inetmon
{
    class ArgsOption
    {
        public string Shortkey;
        public string Value;
        public bool Pair;
        public string Help;
    }

    public class InputOptions
    {
        Dictionary<string, ArgsOption> optionsList = new Dictionary<string, ArgsOption>();

        public bool ValidOptions { get; set; }

        public InputOptions(string[] args)
        {
            optionsList.Add("test",  new ArgsOption { Shortkey = "t", Value = "0", Help = "Testmode - ping every 1 seconds", Pair = false } );
            optionsList.Add("url",   new ArgsOption { Shortkey = "u", Value = "www.google.com", Help = "Url or IP-address to ping", Pair = true });
            optionsList.Add("ping",  new ArgsOption { Shortkey = "p", Value = "60", Help = "Ping interval in seconds", Pair = true });
            optionsList.Add("alive", new ArgsOption { Shortkey = "a", Value = "60", Help = "Show alive interval in minutes", Pair = true });

            ValidOptions = true;

            if (args == null || args.Length == 0)
                return;

            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "-u":
                        case "--url":
                            optionsList["url"].Value = args[i + 1].Trim();
                            break;

                        case "-p":
                        case "--ping":
                            optionsList["ping"].Value = args[i + 1].Trim();
                            break;

                        case "-a":
                        case "--alive":
                            optionsList["alive"].Value = args[i + 1].Trim();
                            break;

                        case "-t":
                        case "--test":
                            optionsList["test"].Value = "1";
                            optionsList["ping"].Value = "1";
                            break;

                        default:
                            ValidOptions = false;
                            break;
                    }
                }
            }
            catch(IndexOutOfRangeException ex)
            {
                ValidOptions = false;
            }
        }

        public string Get(string key)
        {
            return optionsList[key].Value;
        }

        public int GetInt(string key)
        {
            return int.Parse(optionsList[key].Value);
        }
    }
}
