$(function () {

    function AngularPostOfData($http, url, dataToSend, functionName, rawDoneBack, headers) {
        var result = new promiseResult();
        var config = { headers: { 'AppID': AppID } };
        if (!!headers) {
            for (var i in headers) {
                config.headers[i] = headers[i];
            }
        }
        var dataMetrics = { "bucketType": 6, "value": 1 };
        config.headers[XCSN] = XCSNV;
        if (window.avoidFederatedCache)
            config.headers[AVOID_CACHE_HEADER] = '1';
        $http.post(url, dataToSend, config)
            .success(function (data) {
                if (rawDoneBack || data.Success)
                    result.doneCallback(data);
                else {
                    toastr.error(data.Message);
                    AngularPostOfData($http, RegisterMetricsURL, dataMetrics, "Issue").done();
                    if (errorFunction)
                        errorFunction();
                }
                result.alwaysCallback();
            }).error(function (error) {
                ShowHtmlErrorModal("Error in function " + functionName, error);
                if (errorFunction)
                    errorFunction();
                showLoading(false);
                result.alwaysCallback();
            });
        return result;
    }

});