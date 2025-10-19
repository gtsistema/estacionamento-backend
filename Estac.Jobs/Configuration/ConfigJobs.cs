using System;

namespace Estac.Jobs.Configuration
{
    public class ConfigJobs
    {
        public int IntervaloExecucao { get; set; } 
        public TimeSpan TimeExec { get; set; }
        public bool ExecutaJob { get; set; }    
    }
}
