using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Estac.Domain.Auth
{
    public class ApplicationIdentityResult
    {
        public string Message { get; set; }
        public bool Succeeded => _errors.Count == 0;
        private readonly List<string> _errors = new List<string>();

        public ReadOnlyCollection<string> Errors => _errors.AsReadOnly();

        public ApplicationIdentityResult(IEnumerable<string> errors)
        {
            foreach (var error in errors)
                _errors.Add(error);
        }

        public ApplicationIdentityResult(string message)
        {
            Message = message;
        }
    }
}