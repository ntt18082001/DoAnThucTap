import { CardHeader, Skeleton } from '@mui/material';
import { useAppSelector } from 'app/hooks';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import React from 'react';
import { colorMsgDarkmode } from '../../../constants/index';

function SkeletonMessage() {
  const isDarkmode = useAppSelector(selectIsDarkmode);
	const bgColorSkeleton = isDarkmode ? 'grey.700' : '';
	const bgColor = isDarkmode ? 'rgb(255,255,255,.1)' : colorMsgDarkmode;

  return (
    <CardHeader
      sx={{ width: '100%', backgroundColor: bgColor, mb: 0.5 }}
      avatar={
        <Skeleton
          animation="pulse"
          variant="circular"
          width={50}
          height={50}
          sx={{ bgcolor: bgColorSkeleton }}
        />
      }
      title={
        <Skeleton
          animation="pulse"
          height={15}
          width="80%"
          sx={{ bgcolor: bgColorSkeleton, mb: '6px' }}
        />
      }
      subheader={
        <Skeleton animation="pulse" height={12} width="50%" sx={{ bgcolor: bgColorSkeleton }} />
      }
    />
  );
}
export default React.memo(SkeletonMessage);
