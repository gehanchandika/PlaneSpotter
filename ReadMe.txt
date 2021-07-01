# Plane Spotter Configuration

* Web config PlaneSpotter.Web.API
1. Enter the DB Server Name for following section

<add name="PlaneSpotterContext" connectionString="Data Source=<<YourServer Name>>;Initial Catalog=PlaneSpotterAuto;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />

2. Enter the correct service URL as following pattern.
<appSettings>
    ......
    <add key="PlaneSpotterAPIServiceURL" value="https://localhost:44363/v1/spotter/"/>
  </appSettings>

3. Then set both following projects run together. Once the application runs, it will automatically create the data base and add sample records for you.