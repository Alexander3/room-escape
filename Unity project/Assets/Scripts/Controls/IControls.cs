
namespace Scripts
{
    interface IControls
    {
        /// <summary>
        /// Value Range : (-1)-1
        /// </summary>
        float HorizontalMove { get; }
        /// <summary>
        /// Value Range : (-1)-1
        /// </summary>
        float VerticalMove { get; }
        /// <summary>
        /// Value Range : (-1)-1
        /// </summary>
        float HorizontalHead { get; }
        /// <summary>
        /// Value Range : (-1)-1
        /// </summary>
        float VerticalHead { get; }
        /// <summary>
        /// Value Range : (-1)-1 (But usually stops at 0.3)
        /// </summary>
        float ScrollWheel { get; }
        float HorizontalLook { get; }
        float VerticalLook { get; }
        float MouseAbsX { get; }
        float MouseAbsY { get; }
        bool Use { get; }
        bool Jump { get; }
        bool Drop { get; }
        bool Sneaking { get; }
        bool ResetView { get; }
        bool Cancel { get; }
    }
}
