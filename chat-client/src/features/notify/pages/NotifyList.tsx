import { useAppSelector } from 'app/hooks';
import React, { Fragment } from 'react';
import NotifyItem from './NotifyItem';
import SkeletonNotify from './SkeletonNotify';
import { selectListNotify } from '../notifySlice';

function NotifyList() {
  const listNotify = useAppSelector(selectListNotify);

  return (
    <>
      {listNotify.length === 0 ? (
        <Fragment>
          <SkeletonNotify />
          <SkeletonNotify />
          <SkeletonNotify />
          <SkeletonNotify />
        </Fragment>
      ) : (
        listNotify.map((notify) => <NotifyItem key={notify.id} notify={notify} />)
      )}
    </>
  );
}
export default React.memo(NotifyList);
