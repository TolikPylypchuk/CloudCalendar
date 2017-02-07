## SystemJS API

For setting SystemJS configuration see the [Configuration API](config-api.md) page.

#### SystemJS.amdDefine
Type: `Function`

For backwards-compatibility with AMD environments, set `window.define = SystemJS.amdDefine`.

#### SystemJS.amdRequire
Type: `Function`

For backwards-compatibility with AMD environments, set `window.require = SystemJS.amdRequire`.

#### SystemJS.config
Type: `Function`

SystemJS configuration helper function. See the [Configuration API](config-api.md).

#### SystemJS.constructor
Type: `Function`

This represents the System base class, which can be extended or reinstantiated to create a custom System instance.

Example:

```javascript
  var clonedSystemJS = new SystemJS.constructor();
  clonedSystemJS.baseURL = SystemJS.baseURL;
  clonedSystemJS.import('x'); // imports in a custom context
```

#### SystemJS.delete(moduleName) -> Boolean
Type: `Function`

Deletes a module from the registry by normalized name.
Returns true if the module was found in the registry before deletion.

```javascript
SystemJS.delete('http://site.com/normalized/module/name.js');
```

#### SystemJS.get(moduleName) -> Module
Type: `Function`

Returns a module from the registry by normalized name.

```javascript
SystemJS.get('http://site.com/normalized/module/name.js').exportedFunction();
```

#### SystemJS.has(moduleName) -> Boolean
Type: `Function`

Returns whether a given module exists in the registry by normalized module name.

```javascript
if (SystemJS.has('http://site.com/normalized/module/name.js')) {
  // ...
}
```

#### SystemJS.import(moduleName [, normalizedParentName]) -> Promise(Module)
Type: `Function`

Loads a module by name taking an optional normalized parent name argument.

Promise resolves to the module value.

For loading relative to the current module, ES Modules define a `__moduleName` binding, so that:

```javascript
SystemJS.import('./local', __moduleName);
```

In CommonJS modules the above would be `module.id` instead.

This is non-standard, but coverse a use case that will be provided by the spec.

#### SystemJS.newModule(Object) -> Module
Type: `Function`

Given a plain JavaScript object, return an equivalent `Module` object.

Useful when writing a custom `instantiate` hook or using `SystemJS.set`.

#### SystemJS.register([name ,] deps, declare)
Type: `Function`

Declaration function for defining modules of the `System.register` polyfill module format.

[Read more on the format at the loader polyfill page](https://github.com/ModuleLoader/es6-module-loader/blob/v0.17.0/docs/system-register.md)

#### SystemJS.registerDynamic([name ,] deps, executingRequire, declare)
Type: `Function`

Companion module format to `System.register` for non-ES6 modules.

Provides a `<script>`-injection-compatible module format that any CommonJS or Global module can be converted into for CSP compatibility.

Output created by [SystemJS Builder](https://github.com/systemjs/builder) when creating bundles or self-executing bundles.

For example, the following CommonJS module:

```javascript
module.exports = require('pkg/module');
```

Can be written:

```javascript
System.registerDynamic(['pkg/module'], true, function(require, exports, module) {
  module.exports = require('pkg/module');
});
```

`executingRequire` indicates that the dependencies are executed synchronously only when using the `require` function, and not before execution.

* `require` is a standard CommonJS-style require
* `exports` the CommonJS exports object, which is assigned to the `default` export of the module, with its own properties available as named exports.
* `module` represents the CommonJS module object, with `export`, `id` and `url` properties set.

#### SystemJS.set(moduleName, Module)
Type: `Function`

Sets a module into the registry directly and synchronously.

Typically used along with `SystemJS.newModule` to create a valid `Module` object:

```javascript
SystemJS.set('custom-module', SystemJS.newModule({ prop: 'value' }));
```

> Note SystemJS stores all module names in the registry as normalized URLs. To be able to properly use the registry with `SystemJS.set` it is usually necessary to run `SystemJS.set(SystemJS.normalizeSync('custom-module'), SystemJS.newModule({ prop: 'value' }));` to ensure that `SystemJS.import` behaves correctly.

#### SystemJS._nodeRequire
Type: `Function`

In CommonJS environments, SystemJS will substitute the global `require` as needed by the module format being loaded to ensure
the correct detection paths in loaded code.

The CommonJS require can be recovered within these modules from `SystemJS._nodeRequire`.
