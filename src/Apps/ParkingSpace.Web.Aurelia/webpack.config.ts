import * as path from 'path';
import {
  Configuration,
  DefinePlugin,
  SourceMapDevToolPlugin,
  WatchIgnorePlugin
} from 'webpack';
import { AureliaPlugin } from "aurelia-webpack-plugin";
import ForkTsCheckerWebpackPlugin from "fork-ts-checker-webpack-plugin";
import { ForkTsCheckerWebpackPluginOptions } from "fork-ts-checker-webpack-plugin/lib/plugin-options";
import HtmlWebpackPlugin from "html-webpack-plugin";
import TerserPlugin from "terser-webpack-plugin";

// in case you run into any typescript error when configuring `devServer`
import 'webpack-dev-server';

import { CleanWebpackPlugin } from "clean-webpack-plugin";

module.exports = (env, argv): Configuration => {
  const isProd = env.mode === "production";
  const isTest = env.mode === "testing";
  const isDev = env.mode === "development";

  const srcDir = path.resolve(__dirname, "src");
  const outDir = path.resolve(__dirname, "dist");
  const nodeModulesDir = path.resolve(__dirname, "node_modules");

  const config: Configuration = {
    target: 'web',
    context: __dirname,
    mode: isProd ? 'production' : 'development',
    devtool: false,

    resolve: {
      modules: [srcDir, "node_modules"],
      extensions: [".ts", ".js"]
    },

    entry: {
      app: ['aurelia-bootstrapper']
    },

    output: {
      path: outDir,
      publicPath: '',
      filename: isProd || isTest ? "[name].[chunkhash].js" : "[name].js",
      chunkFilename: isProd || isTest ? "[name].[chunkhash].js" : "[name].js",
    },

    devServer: {
      open: false,
      compress: false,
      port: 9000,
      static: {
        directory: outDir,
        publicPath: '/'
      },
      historyApiFallback: true
    },

    module: {
      rules: [
        {
          test: /\.html$/i,
          loader: "html-loader",
        },
        {
          test: /\.ts$/i,
          loader: "ts-loader",
          exclude: nodeModulesDir,
          options: {
            transpileOnly: true
          }
        },
      ]
    },

    plugins: [
      new CleanWebpackPlugin(),
      new WatchIgnorePlugin({ paths: [/scss\.d\.ts$/, "/node_modules/"] }),
      new HtmlWebpackPlugin({
        inject: false,
        title: "Parking space booking",
        template: path.resolve(srcDir, "index.ejs"),
        filename: path.resolve(outDir, "index.html"),
        chunks: ['app'],
        metadata: { baseUrl: "/" },
        minify: false,
        scriptLoading: "blocking"
      }),
      new AureliaPlugin({
        aureliaApp: "main",
      }),
    ],

    optimization: {
      splitChunks: {
        chunks: 'all',
        cacheGroups: {
          default: false,
          framework: {
            test: /[\\/]node_modules[\\/]aurelia-/,
            name: "framework",
            priority: 90,
            enforce: true,
          },
          shared: {
            test: /[\\/]shared[\\/]/,
            name: "shared",
            priority: 20,
            enforce: true,
          },
          vendors: {
            test: /[\\/]node_modules[\\/]/,
            name: "vendors",
            priority: 9,
            enforce: true,
          },
          components: {
            test: /[\\/]components[\\/]/,
            name: "components",
            priority: 8,
            enforce: true,
          },
        }
      },
    }
  };

  const plugins = config.plugins || [];
  const optimization = config.optimization || {};

  if (isTest || isDev) {
    plugins.push(
      new SourceMapDevToolPlugin({
        // Remove this line if you prefer inline source maps
        filename: "[file].map",
        // Point sourcemap entries to the original file locations on disk
        moduleFilenameTemplate: path.relative(outDir, "[resourcePath]"),
      })
    );
  }

  let forkTsCheckerPluginConfig: ForkTsCheckerWebpackPluginOptions | undefined = undefined;

  if (isProd || isTest) {
    config.plugins = plugins.concat([
      new DefinePlugin({
        "process.env.NODE_ENV": JSON.stringify("production"),
      }),
    ]);

    forkTsCheckerPluginConfig = {
      async: false,
      typescript: {
        memoryLimit: 4096
      }
    };

    optimization.minimizer = [
      new TerserPlugin({
        parallel: true,
      }),
    ];
  }

  plugins.push(
    new ForkTsCheckerWebpackPlugin(forkTsCheckerPluginConfig)
  );

  return config;
}
