using SiTef.net.Action.Model;

namespace SiTef.net.Action
{
    public interface IAction<M, N>
        where M : IActionRequest
        where N : IActionResponse
    {
        N Execute(M model);
    }
}
