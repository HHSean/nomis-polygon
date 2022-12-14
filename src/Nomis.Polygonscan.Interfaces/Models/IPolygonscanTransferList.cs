using System.Text.Json.Serialization;

namespace Nomis.Polygonscan.Interfaces.Models
{
    /// <summary>
    /// Polygonscan transfer list.
    /// </summary>
    /// <typeparam name="TListItem">Polygonscan transfer.</typeparam>
    public interface IPolygonscanTransferList<TListItem>
        where TListItem : IPolygonscanTransfer
    {
        /// <summary>
        /// List of transfers.
        /// </summary>
        [JsonPropertyName("result")]
        public List<TListItem> Data { get; set; }
    }
}