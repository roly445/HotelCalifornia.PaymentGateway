var
    webpack = require('webpack'),
    path = require('path'),
    HtmlWebpackPlugin = require("html-webpack-plugin")
    ;

module.exports = {
    entry: {
        cardhost: [
            './Resources/Scripts/card-host/card-host.ts'
        ]
    },
    devtool: 'inline-source-map',
    output: {
        path: path.resolve(__dirname, 'wwwroot'),
        publicPath: "/",
        filename: "[name].js",
        library: "paymentGateway",
        libraryTarget: "var"
    },
    resolve: {
        extensions: ['.ts', '.tsx']
    },
    module: {
        loaders: [
            {
                test: /\.tsx?$/,
                use: [
                    'ts-loader'
                ]
            }
        ]
    },
    plugins: [
        new HtmlWebpackPlugin({
            inject: 'body',
            filename: '../Features/CardHost/CardHost.cshtml',
            template: './Features/CardHost/CardHost_Template.cshtml',
            hash: true,
            files: {
                js: ['cardhost.js']
            },
            chucks: {
                head: {
                    js: ['cardhost.js']
                }
            }
        })
    ]
}