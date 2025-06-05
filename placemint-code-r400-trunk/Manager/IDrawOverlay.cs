
namespace PlaceMint.Manager
{
    /// <summary>
    /// Interface to trigger overlay drawing.
    /// </summary>
    public interface IDrawOverlay
    {
        /// <summary>
        /// Produce the overlay to be drawn.
        /// </summary>
        /// <param name="shape">The shape the overlay should take</param>
        void DrawOverlay(RectangleWrap shape);
    }
}
