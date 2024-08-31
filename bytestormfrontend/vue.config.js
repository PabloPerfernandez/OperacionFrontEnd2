// vue.config.js
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {
  configureWebpack: {
    plugins: [
      new MiniCssExtractPlugin({
        filename: '[name].css',
        chunkFilename: '[id].css',
      }),
    ],
  },
  css: {
    extract: true, // Asegúrate de que la extracción de CSS esté activada
  },
};