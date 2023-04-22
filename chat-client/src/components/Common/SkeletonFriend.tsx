import { Box, Grid, Skeleton } from '@mui/material';
import { useAppSelector } from 'app/hooks';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import React from 'react';

function SkeletonFriend() {
	const isDarkmode = useAppSelector(selectIsDarkmode);
  const bgColorSkeleton = isDarkmode ? 'grey.700' : '';

  return (
    <Grid item xs={3} md={3} sm={6}>
      <Box>
        <Skeleton variant="rectangular" width='100%' height={300} sx={{ bgcolor: bgColorSkeleton }} />
        <Box sx={{ pt: 0.5 }}>
          <Skeleton sx={{ bgcolor: bgColorSkeleton }} />
          <Skeleton width="60%" sx={{ bgcolor: bgColorSkeleton }} />
        </Box>
      </Box>
    </Grid>
  );
}
export default React.memo(SkeletonFriend);
