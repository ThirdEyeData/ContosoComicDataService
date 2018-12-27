<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="DataService" generation="1" functional="0" release="0" Id="64651d68-498e-4762-861e-6a12d9e33162" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="DataServiceGroup" generation="1" functional="0" release="0">
      <settings>
        <aCS name="DataGenerator:APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:APPINSIGHTS_INSTRUMENTATIONKEY" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:AuthorityUri" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:AuthorityUri" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:ClientID" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:ClientID" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:DatasetName" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:DatasetName" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:GroupId" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:GroupId" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:MarketingCost" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:MarketingCost" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:Password" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:Password" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:PowerBIPushURL" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:PowerBIPushURL" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:RedirectUri" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:RedirectUri" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:ResourceUri" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:ResourceUri" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:SQLConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:SQLConnectionString" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:SummaryTableName" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:SummaryTableName" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:TableName" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:TableName" />
          </maps>
        </aCS>
        <aCS name="DataGenerator:UserName" defaultValue="">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGenerator:UserName" />
          </maps>
        </aCS>
        <aCS name="DataGeneratorInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/DataService/DataServiceGroup/MapDataGeneratorInstances" />
          </maps>
        </aCS>
      </settings>
      <maps>
        <map name="MapDataGenerator:APPINSIGHTS_INSTRUMENTATIONKEY" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/APPINSIGHTS_INSTRUMENTATIONKEY" />
          </setting>
        </map>
        <map name="MapDataGenerator:AuthorityUri" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/AuthorityUri" />
          </setting>
        </map>
        <map name="MapDataGenerator:ClientID" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/ClientID" />
          </setting>
        </map>
        <map name="MapDataGenerator:DatasetName" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/DatasetName" />
          </setting>
        </map>
        <map name="MapDataGenerator:GroupId" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/GroupId" />
          </setting>
        </map>
        <map name="MapDataGenerator:MarketingCost" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/MarketingCost" />
          </setting>
        </map>
        <map name="MapDataGenerator:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapDataGenerator:Password" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/Password" />
          </setting>
        </map>
        <map name="MapDataGenerator:PowerBIPushURL" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/PowerBIPushURL" />
          </setting>
        </map>
        <map name="MapDataGenerator:RedirectUri" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/RedirectUri" />
          </setting>
        </map>
        <map name="MapDataGenerator:ResourceUri" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/ResourceUri" />
          </setting>
        </map>
        <map name="MapDataGenerator:SQLConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/SQLConnectionString" />
          </setting>
        </map>
        <map name="MapDataGenerator:SummaryTableName" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/SummaryTableName" />
          </setting>
        </map>
        <map name="MapDataGenerator:TableName" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/TableName" />
          </setting>
        </map>
        <map name="MapDataGenerator:UserName" kind="Identity">
          <setting>
            <aCSMoniker name="/DataService/DataServiceGroup/DataGenerator/UserName" />
          </setting>
        </map>
        <map name="MapDataGeneratorInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/DataService/DataServiceGroup/DataGeneratorInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="DataGenerator" generation="1" functional="0" release="0" software="C:\ThirdEyeCSS\Comics-Embedded Power BI\DataService\DataService\csx\Debug\roles\DataGenerator" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="" />
              <aCS name="AuthorityUri" defaultValue="" />
              <aCS name="ClientID" defaultValue="" />
              <aCS name="DatasetName" defaultValue="" />
              <aCS name="GroupId" defaultValue="" />
              <aCS name="MarketingCost" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="Password" defaultValue="" />
              <aCS name="PowerBIPushURL" defaultValue="" />
              <aCS name="RedirectUri" defaultValue="" />
              <aCS name="ResourceUri" defaultValue="" />
              <aCS name="SQLConnectionString" defaultValue="" />
              <aCS name="SummaryTableName" defaultValue="" />
              <aCS name="TableName" defaultValue="" />
              <aCS name="UserName" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;DataGenerator&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;DataGenerator&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/DataService/DataServiceGroup/DataGeneratorInstances" />
            <sCSPolicyUpdateDomainMoniker name="/DataService/DataServiceGroup/DataGeneratorUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/DataService/DataServiceGroup/DataGeneratorFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="DataGeneratorUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="DataGeneratorFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="DataGeneratorInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
</serviceModel>