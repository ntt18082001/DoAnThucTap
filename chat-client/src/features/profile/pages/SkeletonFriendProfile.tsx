import { Box, Grid, Skeleton } from '@mui/material';
import { useAppSelector } from 'app/hooks';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import React from 'react';

function SkeletonFriendProfile() {
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const bgColorSkeleton = isDarkmode ? 'grey.700' : '';
  return (
    <Grid item xs={6} sm={6}>
      <Box sx={{ display: 'flex' }}>
        <Box sx={{ width: '30%', height: '100px', overflow: 'hidden', mr: 2 }}>
          <Skeleton animation="pulse" width="100%" height="100%" sx={{ bgcolor: bgColorSkeleton }} />
        </Box>
        <Box sx={{ width: '70%', display: 'flex', flexDirection: 'column', justifyContent: 'center' }}>
          <Skeleton
            animation="pulse"
            height={15}
            width="70%"
            sx={{ bgcolor: bgColorSkeleton, mb: '6px' }}
          />
          <Skeleton
            animation="pulse"
            height={15}
            width="50%"
            sx={{ bgcolor: bgColorSkeleton }}
          />
        </Box>
      </Box>
    </Grid>
  );
}

export default React.memo(SkeletonFriendProfile);
