using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class JSNDropMessage
{
    public string Message;
    public string Type;
}

/// <summary>
/// Manages interaction with Game server by sending and receiving JSON data via HTTP protocol.
/// </summary>
public class JSNDrop
{
    private string serverUrl = "";
    private string gameNameId = "";
    private string _result = "";
    private Dictionary<string, string> _tables;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="serverUrl">game server url</param>
    /// <param name="gameNameId">game name identifier</param>
    public JSNDrop(string serverUrl, string gameNameId)
    {
        this.serverUrl = serverUrl;
        this.gameNameId = gameNameId;
    }

    public string Result { get { return _result; } }

    /// <summary>
    /// Register client application's table for syncronization with other client application.
    /// </summary>
    /// <param name="syncTableName">table name</param>
    /// <param name="securityCode">security code for connecting to table</param>
    /// <return>object for Unity3d corutine management</return>
    public IEnumerator jsnReg(string syncTableName, string securityCode)
    {
        var qStr = string.Format("{0}?cmd=jsnReg&value={1},{2},{3}",
            serverUrl,
            gameNameId,
            syncTableName,
            securityCode);

        return SendReg(qStr, syncTableName);
    }

    /// <summary>
    /// Drop sync table for all client applicationw.
    /// </summary>
    /// <param name="syncTableName">table name</param>
    /// <param name="securityCode">security code for connecting to table</param>
    /// <return>object for Unity3d corutine management</return>
    public IEnumerator jsnKill(string syncTableName, string securityCode)
    {
        var qStr = string.Format("{0}?cmd=jsnKill&value={1},{2},{3}",
            serverUrl,
            gameNameId,
            syncTableName,
            securityCode);

        return SendKill(qStr, syncTableName);
    }

    public IEnumerator jsnGet<T>(string pTableName, string pStrPattern, Action<IEnumerable<T>> responseHandler)
    {
        string tblConnectionId = _tables != null ? _tables[pTableName] : "";
        if (string.IsNullOrEmpty(tblConnectionId))
            Debug.Log("JSNDrop: Table hasn't been registered yet: " + pTableName);

        var qStr = string.Empty;
        if (pStrPattern == "")
            qStr = string.Format("{0}?cmd=jsnGet&value={1}&ticks={2}", serverUrl, tblConnectionId, DateTime.Now.Ticks);
        else
            qStr = string.Format("{0}?cmd=jsnGet&value={1},{2}", serverUrl, tblConnectionId, pStrPattern);

        return SendGet<T>(qStr, responseHandler);
    }

    public IEnumerator jsnPut<T>(string pTableName, string pStrKey, T aDTO)
    {
        string tblConnectionId = _tables != null ? _tables[pTableName] : "";
        if (string.IsNullOrEmpty(tblConnectionId))
            Debug.Log("JSNDrop: Table hasn't been registered yet: " + pTableName);

        var jsnStr = JsonUtility.ToJson(aDTO);
        var qStr = string.Format("{0}?cmd=jsnPut&value={1},{2},{3}",
            serverUrl,
            tblConnectionId,
            pStrKey,
            jsnStr);

        return SendPut<T>(qStr);
    }

    public IEnumerator jsnDel(string pTableName, string pStrKey)
    {
        string tblConnectionId = _tables != null ? _tables[pTableName] : "";
        if (string.IsNullOrEmpty(tblConnectionId))
            Debug.Log("JSNDrop: Table hasn't been registered yet: " + pTableName);

        var qStr = string.Format("{0}?cmd=jsnDelete&value={1},{2}",
            serverUrl,
            tblConnectionId,
            pStrKey);

        return SendDel(qStr);
    }

    #region Internal methods

    private IEnumerator SendReg(string requestUrl, string pTblName)
    {
        Debug.Log("REG: " + requestUrl);

        var webReq = UnityWebRequest.Get(requestUrl);
        yield return webReq.Send();

        if (webReq.isError)
        {
            Debug.Log(webReq.error);
        }
        else
        {
            // Show results as text
            Debug.Log(webReq.downloadHandler.text);

            var message = JsonUtility.FromJson<JSNDropMessage>(webReq.downloadHandler.text);
            if (message.Message != "NEW" && message.Message != "EXISTS")
                throw new ApplicationException(
                    string.Format("Failed to register at JsnDrop server: {0}, {1}.",
                        message.Type,
                        message.Message));

            _result = message.Type;

            _tables = _tables ?? new Dictionary<string, string>();

            if (!_tables.ContainsKey(pTblName))
                _tables.Add(pTblName, _result);
        }
    }

    private IEnumerator SendKill(string requestUrl, string pTblName)
    {
        Debug.Log("KILL: " + requestUrl + ", " + pTblName);

        var webReq = UnityWebRequest.Get(requestUrl);
        yield return webReq.Send();

        if (webReq.isError)
        {
            Debug.Log(webReq.error);
        }
        else
        {
            // Show results as text
            Debug.Log(webReq.downloadHandler.text);

            // clear existing connection identifier from local collection
            if (_tables.ContainsKey(pTblName))
                _tables.Remove(pTblName);
        }
    }

    private IEnumerator SendGet<T>(string requestUrl, Action<IEnumerable<T>> responseHandler)
    {
        Debug.Log("SENT_GET: " + requestUrl);

        UnityWebRequest webReq = UnityWebRequest.Get(requestUrl);
        yield return webReq.Send();

        if (webReq.isError)
        {
            Debug.Log(webReq.error);
        }
        else
        {
            // Show results as text
            //Debug.Log("GET Response " + webReq.downloadHandler.text);

            _result = webReq.downloadHandler.text;

            // replace outer {} by []
            if (_result.Length > 2)
            {
                _result = _result.Remove(0, 1);
                _result = "[" + _result;
            }
            if (_result.Length > 2)
            {
                _result = _result.Remove(_result.Length - 1, 1);
                _result = _result + "]";
            }

            responseHandler(GetResultCollection<T>());
        }
    }

    private IEnumerator SendPut<T>(string requestUrl)
    {
        Debug.Log("SENT_PUT: " + requestUrl);

        var webReq = UnityWebRequest.Get(requestUrl);
        yield return webReq.Send();

        if (webReq.isError)
        {
            Debug.Log(webReq.error);
        }
        else
        {
            //Debug.Log("PUT Response " + webReq.downloadHandler.text);

            var message = JsonUtility.FromJson<JSNDropMessage>(webReq.downloadHandler.text);
        }
    }

    private IEnumerator SendDel(string requestUrl)
    {
        Debug.Log("SENT_DEL: " + requestUrl);

        UnityWebRequest webReq = UnityWebRequest.Get(requestUrl);
        yield return webReq.Send();

        if (webReq.isError)
        {
            Debug.Log(webReq.error);
        }
        else
        {
            // Show results as text
            Debug.Log("DEL Response " + webReq.downloadHandler.text);

            _result = webReq.downloadHandler.text;
        }
    }

    private T GetResult<T>() where T : new()
    {
        var res = JsonUtility.FromJson<T>(_result);

        Debug.Log(res.ToString());

        return res;
    }

    private IEnumerable<T> GetResultCollection<T>()
    {
        Debug.Log("Converting json to array of object: " + _result);

        var theList = JsonHelper.getJsonArray<T>(_result);

        return theList;
    }

    #endregion
}


