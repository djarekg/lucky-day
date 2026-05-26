import styles from './page.module.css';

/** Renders the default home page content. */
export default function Home() {
  return (
    <div className={styles.page}>
      <span className={styles.title}>Welcome to Lucky Day!</span>
    </div>
  );
}
