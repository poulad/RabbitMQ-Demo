const $ = require('shelljs')
const path = require('path')
require('../logging')

$.config.fatal = true
const root = path.join(__dirname, '..', '..')


module.exports.clear = function () {
    console.info('Clearing dist directory')

    $.rm('-rf', `${root}/dist`)
    $.mkdir('-p', `${root}/dist/app/`)
}

module.exports.build_app = function () {
    console.info('Building app')

    $.exec(
        `docker run --rm ` +
        `--volume "${root}:/project" ` +
        `--volume "${root}/dist/app:/app" ` +
        `--workdir /project/RabbitMQ-Demo/ ` +
        `microsoft/dotnet:2.1.402-sdk ` +
        `dotnet publish --configuration Release --output /app/`
    )
}
