# <span style="color: #883DAA;font-family: "Helvetica Neue", sans;">react-down ⚛ ⬇</span>
[![npm version](https://badge.fury.io/js/react-down.svg)](https://badge.fury.io/js/react-down)
[![Build Status](https://travis-ci.org/mfellner/react-down.svg?branch=master)](https://travis-ci.org/mfellner/react-down)
[![Coverage Status](https://coveralls.io/repos/github/mfellner/react-down/badge.svg?branch=master)](https://coveralls.io/github/mfellner/react-down?branch=master)

Transform Markdown to React elements.

### Introduction

react-down is a simple library and React component to transform Markdown formatted text into native React elements. react-down uses [markdown-it](https://markdown-it.github.io) to parse Markdown and directly translate it into a structure of React elements.

### Usage

react-down can be used as a function that returns a React element or as a React component:

```javascript
import ReactDown, { transform } from 'react-down'
import ReactDOM from 'react-dom'

const src = `# Hello, Markdown!
This is an *example*.`

const main = document.getElementById('main')

// Using the React component:
ReactDOM.render(<ReactDown src={src}/>, main)

// Using the transform function:
ReactDOM.render(transform(src), main)
```

See the [example](example) for more details.

### Plugins

A plugin is a function that returns a React element. The function is called for each HTML element in the source Markdown and it receives the following arguments:

* `type: string` - HTML tag name (e.g. h1, p, etc.)
* `props: Object` - existing properties (e.g. `key`)
* `children: ?Array<Object>` - existing React child elements
* `token: Object` - Original [markdown-it token](https://markdown-it.github.io/markdown-it/#Token)

```javascript
import React from 'react'
import ReactDown, { combinePlugins, transform } from ' react-down'

function myPlugin(type, props, children, token) {
  if (type === 'h1') {
    return React.createElement('div', null, children)
  }
}

const main = document.getElementById('main')

// Using the React component:
ReactDOM.render(<ReactDown src="..." plugins={[myPlugin]}/>, main)

// Using the transform function:
const myTransform = combinePlugins(myPlugin /*, ... */)(transform)
ReactDOM.render(myTransform(src), main)
```
