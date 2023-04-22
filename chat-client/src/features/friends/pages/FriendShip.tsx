import { Grid, InputAdornment, Typography } from '@mui/material';
import React, { useState } from 'react';
import SearchIcon from '@mui/icons-material/Search';
import { CssTextField } from 'utils/CssTextField';
import { useAppSelector } from 'app/hooks';
import { selectUser } from 'features/auth/authSlice';
import FriendList from './FriendList';
import { useDebounce } from 'hooks/useDebounce';

function FriendShip() {
  const user = useAppSelector(selectUser);
  const [searchTerm, setSearchTerm] = useState('');
  const debounceSearchTerm = useDebounce(searchTerm, 1000);

  return (
    <>
      <Typography variant="h4" gutterBottom>
        Gợi ý kết bạn
      </Typography>
      <CssTextField
        id="search"
        type="search"
        label="Tìm kiếm"
        value={searchTerm}
        onChange={(ev) => setSearchTerm(ev.target.value)}
        sx={{ width: 600, mt: 2 }}
        InputProps={{
          endAdornment: (
            <InputAdornment position="end">
              <SearchIcon color="secondary" />
            </InputAdornment>
          ),
        }}
      />
      <Grid container spacing={2} sx={{ mt: 2 }}>
        <FriendList id={user?.id} searchTerm={debounceSearchTerm} />
      </Grid>
    </>
  );
}

export default React.memo(FriendShip);
