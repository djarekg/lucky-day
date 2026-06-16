'use client';

import { createDOMRenderer, RendererProvider, renderToStyleElements } from '@griffel/react';
import { useServerInsertedHTML } from 'next/navigation';
import { type ReactNode, useState } from 'react';

type GriffelRegistryProps = {
  children: ReactNode;
};

/**
 * Collects griffel styles during server-side rendering and flushes them into the
 * initial HTML, preventing a flash of unstyled content (e.g. grid layouts collapsing
 * to a single column) before client-side hydration injects the styles.
 */
const GriffelRegistry = ({ children }: GriffelRegistryProps) => {
  const [renderer] = useState(() => createDOMRenderer());

  useServerInsertedHTML(() => <>{renderToStyleElements(renderer)}</>);

  return <RendererProvider renderer={renderer}>{children}</RendererProvider>;
};

export default GriffelRegistry;
