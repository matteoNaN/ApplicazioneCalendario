using MudBlazor;
using SharedLibrary.Helpers.ApiResponse;

namespace CalendarioFrontEnd.Utilities
{
    public class ResulToSnakbarParameters
    {
        public string Message { get; set; }
        public Severity Severity { get; set; }

    }
    public static class ParseResultToSnackbar
    {

        public static ResulToSnakbarParameters Parse(Result result, string? OptionaSuccesslMessage = null)
        {
            ResulToSnakbarParameters resulToSnakbarParameters = new ResulToSnakbarParameters();
            Severity sev;
            switch (result.IsSuccess)
            {
                case true:
                    sev = Severity.Success;
                    break;
                case false:
                    sev = Severity.Error;
                    break;
                    default:
                    sev=Severity.Error;
            }

            resulToSnakbarParameters.Severity = sev;

            if(result.IsSuccess == false)
            {
                resulToSnakbarParameters.Message = result.Error.Message;
            }
            else if(!string.IsNullOrEmpty(OptionaSuccesslMessage) && result.IsSuccess)
            {
                resulToSnakbarParameters.Message = OptionaSuccesslMessage;
            }


           return resulToSnakbarParameters;
                
        }


    }
}
