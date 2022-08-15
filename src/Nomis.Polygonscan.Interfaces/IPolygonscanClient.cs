using Nomis.Polygonscan.Interfaces.Models;

namespace Nomis.Polygonscan.Interfaces
{
    /// <summary>
    /// Polygonscan client.
    /// </summary>
    public interface IPolygonscanClient
    {
        /// <summary>
        /// Get the account balance in Wei.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns <see cref="PolygonscanAccount"/>.</returns>
        Task<PolygonscanAccount> GetBalanceAsync(string address);

        /// <summary>
        /// Get list of specific transactions of the given account.
        /// </summary>
        /// <typeparam name="TResult">The type of returned response.</typeparam>
        /// <typeparam name="TResultItem">The type of returned response data items.</typeparam>
        /// <param name="address">Account address.</param>
        /// <returns>Returns list of specific transactions of the given account.</returns>
        Task<IEnumerable<TResultItem>> GetTransactionsAsync<TResult, TResultItem>(string address)
            where TResult : IPolygonscanTransferList<TResultItem>
            where TResultItem : IPolygonscanTransfer;

        /*/// <summary>
        /// Get list of normal transactions of the given account.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns list of normal transactions of the given account.</returns>
        Task<IEnumerable<PolygonscanAccountNormalTransaction>> GetNormalTransactionsAsync(string address);

        /// <summary>
        /// Get list of internal transactions of the given account.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns list of internal transactions of the given account.</returns>
        Task<IEnumerable<PolygonscanAccountInternalTransaction>> GetInternalTransactionsAsync(string address);

        /// <summary>
        /// Get list of ERC-20 tokens transferred by an address of the given account.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns list of ERC-20 tokens transferred by an address of the given account.</returns>
        Task<IEnumerable<PolygonscanAccountERC20TokenEvent>> GetERC20TokenEventsAsync(string address);

        /// <summary>
        /// Get list of ERC-721 tokens transferred by an address of the given account.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns list of ERC-721 tokens transferred by an address of the given account.</returns>
        Task<IEnumerable<PolygonscanAccountERC721TokenEvent>> GetERC721TokenEventsAsync(string address);

        /// <summary>
        /// Get list of ERC-1155 tokens transferred by an address of the given account.
        /// </summary>
        /// <param name="address">Account address.</param>
        /// <returns>Returns list of ERC-1155 tokens transferred by an address of the given account.</returns>
        Task<IEnumerable<PolygonscanAccountERC1155TokenEvent>> GetERC1155TokenEventsAsync(string address);*/
    }
}