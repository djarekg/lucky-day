import styles from './header.module.css';

const Header = () => {
  return (
    <header className={styles.header}>
      <span className={styles.title}>Lucky Day</span>
      <img
        src="/lucky-day.svg"
        alt="Lucky Day Logo"
        className={styles.logo}
      />
    </header>
  );
};

export default Header;
