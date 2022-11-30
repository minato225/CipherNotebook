﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using IHostEnv = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CipherNoteBook.Server.Service;

public class WritableOptions<T> : IWritableOptions<T> where T : class, new()
{
    [Obsolete]
    private readonly IHostEnv _environment;
    private readonly IOptionsMonitor<T> _options;
    private readonly IConfigurationRoot _configuration;
    private readonly string _section;
    private readonly string _file;

    [Obsolete]
    public WritableOptions(
        IHostEnv environment,
        IOptionsMonitor<T> options,
        IConfigurationRoot configuration,
        string section,
        string file)
    {
        _environment = environment;
        _options = options;
        _configuration = configuration;
        _section = section;
        _file = file;
    }

    public T Value => _options.CurrentValue;
    public T Get(string name) => _options.Get(name);

    [Obsolete]
    public void ChangeAppSettingValue(Action<T> applyChanges)
    {
        var fileProvider = _environment.ContentRootFileProvider;
        var fileInfo = fileProvider.GetFileInfo(_file);
        var physicalPath = fileInfo.PhysicalPath;

        var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath));
        var sectionObject = jObject.TryGetValue(_section, out JToken section) ?
            JsonConvert.DeserializeObject<T>(section.ToString()) : Value ?? new T();

        applyChanges(sectionObject);

        jObject[_section] = JObject.Parse(JsonConvert.SerializeObject(sectionObject));
        File.WriteAllText(physicalPath, JsonConvert.SerializeObject(jObject, Formatting.Indented));
        _configuration.Reload();
    }
}