<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AzureCloudServiceTeamProject" generation="1" functional="0" release="0" Id="8cbbd707-ba2f-430e-b669-7722ca30cdbc" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AzureCloudServiceTeamProjectGroup" generation="1" functional="0" release="0">
      <settings>
        <aCS name="MyTeamWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudServiceTeamProject/AzureCloudServiceTeamProjectGroup/MapMyTeamWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="MyTeamWorkerRole:StorageConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureCloudServiceTeamProject/AzureCloudServiceTeamProjectGroup/MapMyTeamWorkerRole:StorageConnectionString" />
          </maps>
        </aCS>
        <aCS name="MyTeamWorkerRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AzureCloudServiceTeamProject/AzureCloudServiceTeamProjectGroup/MapMyTeamWorkerRoleInstances" />
          </maps>
        </aCS>
      </settings>
      <maps>
        <map name="MapMyTeamWorkerRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudServiceTeamProject/AzureCloudServiceTeamProjectGroup/MyTeamWorkerRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapMyTeamWorkerRole:StorageConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureCloudServiceTeamProject/AzureCloudServiceTeamProjectGroup/MyTeamWorkerRole/StorageConnectionString" />
          </setting>
        </map>
        <map name="MapMyTeamWorkerRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AzureCloudServiceTeamProject/AzureCloudServiceTeamProjectGroup/MyTeamWorkerRoleInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="MyTeamWorkerRole" generation="1" functional="0" release="0" software="C:\Users\Christian\source\repos\AzureCloudServiceTeamProject\AzureCloudServiceTeamProject\csx\Debug\roles\MyTeamWorkerRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="StorageConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;MyTeamWorkerRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;MyTeamWorkerRole&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AzureCloudServiceTeamProject/AzureCloudServiceTeamProjectGroup/MyTeamWorkerRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/AzureCloudServiceTeamProject/AzureCloudServiceTeamProjectGroup/MyTeamWorkerRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/AzureCloudServiceTeamProject/AzureCloudServiceTeamProjectGroup/MyTeamWorkerRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="MyTeamWorkerRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="MyTeamWorkerRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="MyTeamWorkerRoleInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
</serviceModel>