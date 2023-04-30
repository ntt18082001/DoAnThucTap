import { Box, Skeleton } from '@mui/material';
import React from 'react';

function SkeletonImage() {
  return (
    <Box sx={{ pt: 0.5 }}>
      <Skeleton height="150px" />
    </Box>
  );
}
export default React.memo(SkeletonImage);
