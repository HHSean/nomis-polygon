using System.Net;

using Nethereum.Util;
using Nomis.Polygonscan.Calculators;
using Nomis.Polygonscan.Interfaces;
using Nomis.Polygonscan.Interfaces.Models;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.Polygonscan
{
    /// <inheritdoc cref="IPolygonscanService"/>
    internal sealed class PolygonscanService :
        IPolygonscanService,
        ITransientService
    {
        /// <summary>
        /// Initialize <see cref="PolygonscanService"/>.
        /// </summary>
        /// <param name="client"><see cref="IPolygonscanClient"/>.</param>
        public PolygonscanService(
            IPolygonscanClient client)
        {
            Client = client;
        }

        /// <inheritdoc/>
        public IPolygonscanClient Client { get; }

        /// <inheritdoc/>
        public async Task<Result<PolygonWalletScore>> GetWalletStatsAsync(string address)
        {
            if (!new AddressUtil().IsValidAddressLength(address) || !new AddressUtil().IsValidEthereumAddressHexFormat(address))
            {
                throw new CustomException("Invalid address", statusCode: HttpStatusCode.BadRequest);
            }

            var balanceWei = (await Client.GetBalanceAsync(address)).Balance;
            var transactions = (await Client.GetTransactionsAsync<PolygonscanAccountNormalTransactions, PolygonscanAccountNormalTransaction>(address)).ToList();
            var internalTransactions = (await Client.GetTransactionsAsync<PolygonscanAccountInternalTransactions, PolygonscanAccountInternalTransaction>(address)).ToList();
            var erc20Tokens = (await Client.GetTransactionsAsync<PolygonscanAccountERC20TokenEvents, PolygonscanAccountERC20TokenEvent>(address)).ToList();
            var erc721Tokens = (await Client.GetTransactionsAsync<PolygonscanAccountERC721TokenEvents, PolygonscanAccountERC721TokenEvent>(address)).ToList();
            var erc1155Tokens = (await Client.GetTransactionsAsync<PolygonscanAccountERC1155TokenEvents, PolygonscanAccountERC1155TokenEvent>(address)).ToList();

            var tokens = new List<IPolygonscanAccountNftTokenEvent>();
            tokens.AddRange(erc721Tokens);
            tokens.AddRange(erc1155Tokens);

            var walletStats = new PolygonStatCalculator(
                    address,
                    ulong.TryParse(balanceWei, out var wei) ? wei : 0,
                    transactions,
                    internalTransactions,
                    tokens,
                    erc20Tokens)
                .GetStats();

            return await Result<PolygonWalletScore>.SuccessAsync(new()
            {
                Stats = walletStats,
                Score = walletStats.GetScore()
            }, "Got polygon wallet score.");
        }
    }
}