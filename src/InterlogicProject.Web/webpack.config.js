const fs = require("fs");
const path = require("path");
const ProgressPlugin = require("webpack/lib/ProgressPlugin");
const CircularDependencyPlugin = require("circular-dependency-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const autoprefixer = require("autoprefixer");
const postcssUrl = require("postcss-url");
const cssnano = require("cssnano");

const {
	NamedModulesPlugin, NoEmitOnErrorsPlugin,
	ProvidePlugin, SourceMapDevToolPlugin
} = require("webpack");

const {
	BaseHrefWebpackPlugin, GlobCopyWebpackPlugin, NamedLazyChunksWebpackPlugin
} = require("@angular/cli/plugins/webpack");

const { CommonsChunkPlugin } = require("webpack").optimize;
const { AotPlugin } = require("@ngtools/webpack");

const nodeModules = path.join(process.cwd(), "node_modules");
const realNodeModules = fs.realpathSync(nodeModules);

const genDirNodeModules = path.join(
	process.cwd(), "Client", "$$_gendir", "node_modules");

const minimizeCss = false;
const baseHref = "";
const deployUrl = "";

const postcssPlugins = function () {
	const importantCommentRe = /@preserve|@license|[@#]\s*source(?:Mapping)?URL|^!/i;
	const minimizeOptions = {
		autoprefixer: false,
		safe: true,
		mergeLonghand: false,
		discardComments: { remove: (comment) => !importantCommentRe.test(comment) }
	};
	return [
		postcssUrl({
			url: (URL) => {
				if (!URL.startsWith('/') || URL.startsWith('//')) {
					return URL;
				}
				if (deployUrl.match(/:\/\//)) {
					return `${deployUrl.replace(/\/$/, '')}${URL}`;
				}
				else if (baseHref.match(/:\/\//)) {
					return baseHref.replace(/\/$/, '') +
						`/${deployUrl}/${URL}`.replace(/\/\/+/g, '/');
				}
				else {
					return `/${baseHref}/${deployUrl}/${URL}`.replace(/\/\/+/g, '/');
				}
			}
		}),
		autoprefixer()
	].concat(minimizeCss ? [cssnano(minimizeOptions)] : []);
};

const stylePaths = [
	path.join(process.cwd(), "node_modules/bootstrap/dist/css/bootstrap.css"),
	path.join(process.cwd(), "node_modules/bootstrap/dist/css/bootstrap-grid.css"),
	path.join(process.cwd(), "node_modules/bootstrap/dist/css/bootstrap-reboot.css"),
	// path.join(process.cwd(), "node_modules/font-awesome/css/font-awesome.css"),
	path.join(process.cwd(), "node_modules/fullcalendar/dist/fullcalendar.css"),
	path.join(process.cwd(), "Client/css/style.css")
];

module.exports = {
	resolve: {
		extensions: [
			".ts",
			".js"
		],
		modules: [
			"./node_modules",
			"./node_modules"
		],
		symlinks: true
	},
	resolveLoader: {
		modules: [
			"./node_modules",
			"./node_modules"
		]
	},
	entry: {
		main: [
			"./Client/main.ts"
		],
		polyfills: [
			"./Client/polyfills.ts"
		],
		styles: [
			"./node_modules/bootstrap/dist/css/bootstrap.css",
			"./node_modules/bootstrap/dist/css/bootstrap-grid.css",
			"./node_modules/bootstrap/dist/css/bootstrap-reboot.css",
			// "./node_modules/font-awesome/css/font-awesome.css",
			"./node_modules/fullcalendar/dist/fullcalendar.css",
			"./Client/css/style.css"
		]
	},
	output: {
		path: path.join(process.cwd(), "wwwroot/dist"),
		filename: "[name].bundle.js",
		chunkFilename: "[id].chunk.js",
		publicPath: "/app/"
  	},
	module: {
		rules: [
			{
				enforce: "pre",
				test: /\.js$/,
				loader: "source-map-loader",
				exclude: [
					/(\\|\/)node_modules(\\|\/)/
				]
			},
			{
				test: /\.html$/,
				loader: "raw-loader"
			},
			{
				test: /\.(eot|svg|cur)$/,
				loader: "file-loader?name=[name].[hash:20].[ext]"
			},
			{
				test: /\.(jpg|png|webp|gif|otf|ttf|woff|woff2|ani)$/,
				loader: "url-loader?name=[name].[hash:20].[ext]&limit=10000"
			},
			{
				exclude: stylePaths,
				test: /\.css$/,
				use: [
					"exports-loader?module.exports.toString()",
					{
						loader: "css-loader",
						options: {
							sourceMap: false,
							importLoaders: 1
						}
					},
					{
						loader: "postcss-loader",
						options: {
							ident: "postcss",
							plugins: postcssPlugins
						}
					}
				]
			},
			{
				exclude: stylePaths,
				test: /\.scss$|\.sass$/,
				use: [
					"exports-loader?module.exports.toString()",
					{
						loader: "css-loader",
						options: {
							sourceMap: false,
							importLoaders: 1
						}
					},
					{
						loader: "postcss-loader",
						options: {
							ident: "postcss",
							plugins: postcssPlugins
						}
					},
					{
						loader: "sass-loader",
						options: {
							sourceMap: false,
							precision: 8,
							includePaths: []
						}
					}
				]
			},
			{
				exclude: stylePaths,
				test: /\.less$/,
				use: [
					"exports-loader?module.exports.toString()",
					{
						loader: "css-loader",
						options: {
							sourceMap: false,
							importLoaders: 1
						}
					},
					{
						loader: "postcss-loader",
						options: {
							ident: "postcss",
							plugins: postcssPlugins
						}
					},
					{
						loader: "less-loader",
						options: {
							sourceMap: false
						}
					}
				]
			},
			{
				exclude: stylePaths,
				test: /\.styl$/,
				use: [
					"exports-loader?module.exports.toString()",
					{
						loader: "css-loader",
						options: {
							sourceMap: false,
							importLoaders: 1
						}
					},
					{
						loader: "postcss-loader",
						options: {
							ident: "postcss",
							plugins: postcssPlugins
						}
					},
					{
						loader: "stylus-loader",
						options: {
							sourceMap: false,
							paths: []
						}
					}
				]
			},
			{
				include: stylePaths,
				test: /\.css$/,
				use: [
					"style-loader",
					{
						loader: "css-loader",
						options: {
							sourceMap: false,
							importLoaders: 1
						}
					},
					{
						loader: "postcss-loader",
						options: {
							ident: "postcss",
							plugins: postcssPlugins
						}
					}
				]
			},
			{
				include: stylePaths,
				test: /\.scss$|\.sass$/,
				use: [
					"style-loader",
					{
						loader: "css-loader",
						options: {
							sourceMap: false,
							importLoaders: 1
						}
					},
					{
						loader: "postcss-loader",
						options: {
							ident: "postcss",
							plugins: postcssPlugins
						}
					},
					{
						loader: "sass-loader",
						options: {
							sourceMap: false,
							"precision": 8,
							"includePaths": []
						}
					}
				]
			},
			{
				include: stylePaths,
				test: /\.less$/,
				use: [
					"style-loader",
					{
						loader: "css-loader",
						options: {
							sourceMap: false,
							importLoaders: 1
						}
					},
					{
						loader: "postcss-loader",
						options: {
							ident: "postcss",
							plugins: postcssPlugins
						}
					},
					{
						loader: "less-loader",
						options: {
							sourceMap: false
						}
					}
				]
			},
			{
				include: stylePaths,
				test: /\.styl$/,
				use: [
					"style-loader",
					{
						loader: "css-loader",
						options: {
							sourceMap: false,
							importLoaders: 1
						}
					},
					{
						loader: "postcss-loader",
						options: {
							ident: "postcss",
							plugins: postcssPlugins
						}
					},
					{
						loader: "stylus-loader",
						options: {
							sourceMap: false,
							paths: []
						}
					}
				]
			},
			{
				test: /\.ts$/,
				loader: "@ngtools/webpack"
			}
		]
	},
	plugins: [
		new NoEmitOnErrorsPlugin(),
		new GlobCopyWebpackPlugin({
			patterns: [
				"assets",
				"favicon.ico"
			],
			globOptions: {
				cwd: path.join(process.cwd(), "Client"),
				dot: true,
				ignore: "**/.gitkeep"
			}
		}),
		new ProgressPlugin(),
		new CircularDependencyPlugin({
			exclude: /(\\|\/)node_modules(\\|\/)/,
			failOnError: false
		}),
		new NamedLazyChunksWebpackPlugin(),
		new ProvidePlugin({
			$: "jquery",
			jQuery: "jquery",
			"window.jQuery": "jquery",
			Popper: [ "popper.js", "default" ]
		}),
		new BaseHrefWebpackPlugin({ }),
		new CommonsChunkPlugin({
			name: [
				"inline"
			],
			minChunks: null
		}),
		new CommonsChunkPlugin({
			name: [
				"vendor"
			],
			minChunks: (module) => {
				return module.resource
					&& (module.resource.startsWith(nodeModules)
					|| module.resource.startsWith(genDirNodeModules)
					|| module.resource.startsWith(realNodeModules));
			},
			chunks: [
				"main"
			]
		}),
		new SourceMapDevToolPlugin({
			filename: "[file].map[query]",
			moduleFilenameTemplate: "[resource-path]",
			fallbackModuleFilenameTemplate: "[resource-path]?[hash]",
			sourceRoot: "webpack:///"
		}),
		new CommonsChunkPlugin({
			name: [
				"main"
			],
			minChunks: 2,
			async: "common"
		}),
		new NamedModulesPlugin({ }),
		new AotPlugin({
			mainPath: "main.ts",
			replaceExport: false,
			hostReplacementPaths: {
				"environments/environment.ts": "environments/environment.ts"
			},
			exclude: [],
			tsConfigPath: "Client/tsconfig.app.json",
			skipCodeGeneration: true
		})
	],
	node: {
		fs: "empty",
		global: true,
		crypto: "empty",
		tls: "empty",
		net: "empty",
		process: true,
		module: false,
		clearImmediate: false,
		setImmediate: false
	},
	devServer: {
		historyApiFallback: true
	}
};
