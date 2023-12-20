
namespace Domain.GeneralModels
{
    public class ServiceResult
    {
        public ServiceResult() { }

        public ServiceResult(bool status = true, string explanation = "")
        {
            Status = status;
            Explanation = explanation;
        }

        public bool Status { get; set; }
        public string Explanation { get; set; }
    }

    public class ServiceResultExt<T> : ServiceResult
    {
        public ServiceResultExt() { }

        public ServiceResultExt(T resultObject, int? totalCount = null, bool status = true, string explanation = "")
        {
            Status = status;
            Explanation = explanation;
            ResultObject = resultObject;
            TotalCount = totalCount;
        }

        public int? TotalCount { get; set; }
        public T ResultObject { get; set; }
    }


}
