import { autoinject } from "aurelia-framework";
import { Router, RouterConfiguration, NavigationInstruction, RouteConfig } from "aurelia-router";

import { routes } from "config";

@autoinject
export class App {
  constructor(
    protected router: Router
  ) {
  }

  async activate(params, route: RouteConfig, navigationInstruction: NavigationInstruction) {
  }

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = "Aurelia chat client";

    config.options.pushState = true;

    config.map(routes);

    this.router = router;
  }
}
