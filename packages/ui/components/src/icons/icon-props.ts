import type { SVGProps } from 'react';

/**
 * This type is used for the props of the icon components, extending React's SVGProps.
 */
export type IconProps = {
  className?: string;
  size?: number;
} & SVGProps<SVGSVGElement>;
