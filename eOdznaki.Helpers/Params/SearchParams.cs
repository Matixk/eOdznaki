namespace eOdznaki.Helpers.Params
{
    public class SearchParams : Params
    {
        public SearchParams()
        {
            MaxPageSize = 50;
            PageSize = 10;
        }

        public string Regex { get; set; }
    }
}