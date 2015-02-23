Raygun Setup

1. Add a section to configSections:
<section name="RaygunSettings" type="Mindscape.Raygun4Net.RaygunSettings, Mindscape.Raygun4Net"/>

2. Add the Raygun settings configuration block from above:
<RaygunSettings apikey="YOUR_APP_API_KEY" />

Send exceptions Automatically or Manually
- Automatically
For system.web:

<httpModules>
  <add name="RaygunErrorModule" type="Mindscape.Raygun4Net.RaygunHttpModule"/>
</httpModules>
For system.webServer:

<modules>
  <add name="RaygunErrorModule" type="Mindscape.Raygun4Net.RaygunHttpModule"/>
</modules>

- Manually
try
{

}
catch (Exception e)
{
  new RaygunClient().SendInBackground(e);
}

OR in own handler:

protected void Application_Error()
{
  var exception = Server.GetLastError();
  new RaygunClient().Send(exception);
}