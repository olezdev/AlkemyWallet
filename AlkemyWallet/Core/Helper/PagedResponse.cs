using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Core.Helper
{
    public class PagedResponse
    {
        public string nextPage { get; set; }
        public string previousPage { get; set; }
        public int pageIndex { get; set; }
        public int totalPages { get; set; }
        public List<TransactionsDTO> data { get; set; }
    }
}
