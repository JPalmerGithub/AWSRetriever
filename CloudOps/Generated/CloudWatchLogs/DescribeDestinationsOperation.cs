using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.CloudWatchLogs.Model;
using Amazon.Runtime;

namespace CloudOps.CloudWatchLogs
{
    public class DescribeDestinationsOperation : Operation
    {
        public override string Name => "DescribeDestinations";

        public override string Description => "Lists all your destinations. The results are ASCII-sorted by destination name.";
 
        public override string RequestURI => "/";

        public override string Method => "POST";

        public override string ServiceName => "CloudWatchLogs";

        public override string ServiceID => "CloudWatch Logs";

        public override void Invoke(AWSCredentials creds, RegionEndpoint region, int maxItems)
        {
            AmazonCloudWatchLogsClient client = new AmazonCloudWatchLogsClient(creds, region);
            DescribeDestinationsResponse resp = new DescribeDestinationsResponse();
            do
            {
                DescribeDestinationsRequest req = new DescribeDestinationsRequest
                {
                    NextToken = resp.NextToken
                    ,
                    Limit = maxItems
                                        
                };

                resp = client.DescribeDestinations(req);
                CheckError(resp.HttpStatusCode, "200");                
                
                foreach (var obj in resp.Destinations)
                {
                    AddObject(obj);
                }
                
            }
            while (!string.IsNullOrEmpty(resp.NextToken));
        }
    }
}