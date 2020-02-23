using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    /// <summary>
    /// result interface returns from the command execution
    /// </summary>
    public interface ICommandResult
    {
        bool Success { get; }
        string ErrorMessage { get; }
    }

    /// <summary>
    /// a command interface 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public interface ICommand<T1, T2> where T1 : ICommandResult
    {
        string CommandKey { get; }
        T1 Execute(T2 input, params string[] arguments);
    }
}