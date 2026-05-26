import { Suspense } from 'react';

import Header from '@/components/layout/header/header';

const Layout = ({ children }: { children: React.ReactNode }) => {
  return (
    <>
      <Suspense>
        <Header />
      </Suspense>
      {children}
    </>
  );
};

export default Layout;
