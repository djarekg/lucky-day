/** Checks if the current environment is a browser. */
const browserWindow = (globalThis as { window?: { document?: unknown } }).window;

const workerScope = (globalThis as { self?: { constructor?: { name?: string } } }).self;

export const isBrowser =
  typeof browserWindow !== 'undefined' && typeof browserWindow.document !== 'undefined';

/** Checks if the current environment is a web worker. */
export const isWebWorker =
  typeof workerScope === 'object' &&
  typeof workerScope?.constructor !== 'undefined' &&
  workerScope.constructor.name === 'DedicatedWorkerGlobalScope';

/** Checks if the current environment is a server (Node.js or similar). */
export const isServer = !isBrowser && !isWebWorker;
