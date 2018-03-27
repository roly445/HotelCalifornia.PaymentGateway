var
    //webpack = require('webpack'),
    path = require('path'),
    CleanWebpackPlugin = require('clean-webpack-plugin')
    ;

module.exports = {
    entry: {
        cardhost: [
            './Resources/Scripts/card-host/card-host.ts'
        ]
    },
    //devtool: 'inline-source-map',
    output: {
        path: path.resolve(__dirname, 'wwwroot'),
        filename: "[name].js"        
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
        new CleanWebpackPlugin(['wwwroot'])
    ]
}