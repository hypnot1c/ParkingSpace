import { Aurelia, FrameworkConfiguration, PLATFORM } from "aurelia-framework";

export async function configure(aurelia: Aurelia) {
  let aureliaConfig = aurelia.use
    .standardConfiguration()
    ;

  aureliaConfig = configureLogging(aureliaConfig);
  await aurelia.start();
  await aurelia.setRoot(PLATFORM.moduleName("app"));
}

function configureLogging(frameworkConfig: FrameworkConfiguration): FrameworkConfiguration {
  let result = frameworkConfig;
  result = result.developmentLogging();

  return result;
}
