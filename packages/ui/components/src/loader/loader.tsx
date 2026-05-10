import { makeStyles } from '@griffel/react';
import { CircularProgress } from '@mui/material';

const useStyles = makeStyles({
  container: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    blockSize: '100%',
    inlineSize: '100%',
  },
});

const Loader = () => {
  const styles = useStyles();

  return (
    <div className={styles.container}>
      <CircularProgress />
    </div>
  );
};

export default Loader;
