namespace Nomis.Polygonscan.Interfaces.Models
{
    /// <summary>
    /// Polygon wallet score.
    /// </summary>
    public class PolygonWalletScore
    {
        /// <summary>
        /// Nomis Score in range of [0; 1].
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Additional stat data used in score calculations.
        /// </summary>
        public PolygonWalletStats? Stats { get; set; }
    }
}