namespace MessageQueues.Core.StateManagement
{
    public enum StateRequestType
    {
        Configuration,
        State
    }

    public sealed class StateRequestMessage : TransferableModel
    {
        public StateRequestType RequestType { get; set; }
    }
}
