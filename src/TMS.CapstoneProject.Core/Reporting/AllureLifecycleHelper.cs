using Allure.Net.Commons;
using TMS.CapstoneProject.Core.Logging;

namespace TMS.CapstoneProject.Core.Reporting;

public static class AllureLifecycleHelper
{
    public static void Step(string name, Action action)
    {
        AllureApi.Step(name, () =>
        {
            TestLogger.Info(name);
            action();
        });
    }

    public static T Step<T>(string name, Func<T> action)
    {
        return AllureApi.Step(name, () =>
        {
            TestLogger.Info(name);
            return action();
        });
    }

    public static void AttachText(string name, string content)
    {
        AllureApi.AddAttachment(name, "text/plain", System.Text.Encoding.UTF8.GetBytes(content), ".txt");
    }
}
