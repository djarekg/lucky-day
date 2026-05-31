import { Link } from '@mui/material';

import AppLogoIcon from '@/components/icons/app-logo';
import SiteNav from '@/components/site-nav/site-nav';

import styles from './header.module.css';

const Header = () => {
  return (
    <header className={styles.header}>
      <Link href="/">
        <AppLogoIcon
          className={styles.logo}
          size={40}
        />
      </Link>
      <span className={styles.title}>Lucky Day</span>
      <SiteNav />
    </header>
  );
};

export default Header;
