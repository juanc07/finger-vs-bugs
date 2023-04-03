using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnalytics
{
    void Init();

    void LogEvent(string eventName);

    void LogEvent(string eventName, string parameterName, string value);

    void LogEvent(string eventName, string parameterName, int value);

    void LogEvent(string eventName, string parameterName, double value);

    void LogEvent(string eventName, string parameterName, long value);

    void LogEvent(string eventName, Dictionary< string, object > parameters);


    void SetUserProperty(string propertyName, string value);

    void SetUserId(string value);
}
