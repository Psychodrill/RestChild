namespace RestChild.Comon
{
    public class PagerState
    {
        public PagerState(int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            PerPage = pageSize;
            if (pageNumber == 0)
            {
                SkipNumber = 0;
            }
            else
            {
                SkipNumber = (CurrentPage - 1) * PerPage;
            }
        }

        public int PerPage { get; }

        public int CurrentPage { get; }

        public int TotalCount { get; private set; }

        public int SkipNumber { get; }

        public bool IsEmpty => PerPage == 0;

        public void SetTotalCount(int count)
        {
            TotalCount = count;
        }
    }
}
