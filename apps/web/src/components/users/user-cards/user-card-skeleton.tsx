import { makeStyles } from '@griffel/react';
import { Card, CardActions, CardContent, CardHeader, Skeleton } from '@mui/material';

const CARD_MAX_WIDTH = 375;

const useStyles = makeStyles({
  container: {
    display: 'grid',
    gridTemplateColumns: 'repeat(auto-fill, minmax(300px, 1fr))',
    gap: '1rem',
    alignItems: 'stretch',
  },
  skeletonFullnameContainer: {
    display: 'flex',
    flexDirection: 'column',
    gap: '0.5rem',
  },
});

const UserCardSkeleton = ({ key }: { key: number }) => {
  const styles = useStyles();

  return (
    <Card
      key={key}
      sx={{ maxWidth: CARD_MAX_WIDTH, marginBottom: 2 }}>
      <CardHeader
        avatar={
          <Skeleton
            variant="circular"
            width={40}
            height={40}
          />
        }
        title={
          <Skeleton
            width={150}
            height={23}
          />
        }
        subheader={
          <Skeleton
            width={220}
            height={24}
          />
        }
      />
      <CardContent>
        <div className={styles.skeletonFullnameContainer}></div>
      </CardContent>
      <CardActions>
        <Skeleton
          sx={theme => ({ backgroundColor: theme.palette.primary.main })}
          width={100}
          height={24}
        />
      </CardActions>
    </Card>
  );
};

export default UserCardSkeleton;
